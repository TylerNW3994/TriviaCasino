import React, { useState } from "react";
import { useGameSession } from "./GameSessionProvider";
import { useUser } from './UserProvider';

export default function Blackjack() {
  let defaultGameState = {};
  const { user } = useUser();

  if (user.username) {
    defaultGameState = {
      CurrentPlayer: user,
      DealerHand: [],
      Players: {}
    };
  } else {
    defaultGameState = {
      CurrentPlayer: "",
      DealerHand: [],
      Players: {}
    };
  }

  const [gameState, setGameState] = useState(defaultGameState);
  const { setSessionData } = useGameSession();
  const gameInSession = gameState && gameState.currentPlayer !== "";

  let playerActionButtons, message = "";
  if (gameInSession) {
    playerActionButtons = (
      <>
        <button onClick={() => handleHit(gameState.currentPlayer)}>
          Hit
        </button>
        <button onClick={() => handleStand(gameState.currentPlayer)}>
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
    const response = await fetch("/api/blackjack/newgame", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(defaultGameState)
    });

    processData(JSON.parse(response));
  }

  async function handleHit(username) {
    const response = await fetch("/api/blackjack/hit", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        gameState,
        username: username,
      }),
    });

    processData(JSON.parse(response));
  }

  async function handleStand(username) {
    const response = await fetch("/api/blackjack/stand", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        gameState,
        username: username,
      }),
    });

    processData(JSON.parse(response));
  }

  function processData(data) {
    try {
      console.log(data);
      setGameState(data);
      setSessionData(data);
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
          {gameState.Players && (
            <div>
              <h2>Dealer's Hand:</h2>
              <p>{JSON.stringify(gameState.dealerHand)}</p>
              {Object.entries(gameState.Players).map(([name, player]) => (
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
