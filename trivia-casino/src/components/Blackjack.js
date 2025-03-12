import React, { useState } from "react";
import { useGameSession } from "./GameSessionProvider";
import { useUser } from './UserProvider';

export default function Blackjack() {
  let defaultGameState = {
    gameId: "",
    currentPlayer: "",
    dealerHand: [],
    players: []
  },
  payload = {};

  const { user } = useUser();
  const [gameState, setGameState] = useState(defaultGameState);
  const [message, setMessage] = useState("");
  const { setSessionData } = useGameSession();
  let gameInSession = gameState?.currentPlayer !== "";

  payload.Username = user?.username;
  payload.GameState = defaultGameState;

  let playerActionButtons;
  if (gameInSession) {
    playerActionButtons = (
      <>
        <button onClick={() => handleHit(user?.username)}>
          Hit
        </button>
        <button onClick={() => handleStand(user?.username)}>
          Stand
        </button>
      </>
    );
  } else {
    playerActionButtons = (
      <>
        <button onClick={startNewGame}>New Game</button>
      </>
    );
  }

  async function startNewGame() {
    payload.UserToAdd = user;

    const response = await fetch("/api/blackjack/newgame", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  async function handleHit(username) {
    payload.Username = username;

    const response = await fetch("/api/blackjack/hit", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  async function handleStand(username) {
    payload.Username = username;

    const response = await fetch("/api/blackjack/stand", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  function processData(data) {
    try {
      payload.GameState = data.gameData;
      setGameState(data.gameData);
      setMessage(data.message);
      setSessionData(data.gameData);
    } catch (error) {
      console.error("Error parsing JSON:", error);
    }
  }

  return (
    <div>
      {gameState && (
        <div>
          {message}
          {playerActionButtons}
          {gameState.players && (
            <div>
              <h2>Dealer's Hand:</h2>
              <p>{JSON.stringify(gameState.dealerHand)}</p>
              {Object.entries(gameState.players).map(([name, player]) => (
                <div key={name}>
                  <h3>{name}</h3>
                  <p>{player.Score}</p>
                  <div>Hand: {player.hand && player.hand.join(", ")}</div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
}
