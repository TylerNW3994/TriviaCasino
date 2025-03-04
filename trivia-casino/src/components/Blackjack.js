import React, { useState } from "react";
import { useGameSession } from "./GameSessionProvider";

const defaultGameState = {
  currentPlayer: '',
  dealerHand: [],
  Players: {}
};

export default function Blackjack() {
  const [gameState, setGameState] = useState(defaultGameState);
  const { setSessionData } = useGameSession();
  const gameInSession = gameState && gameState.currentPlayer !== '';

  let playerActionButtons;
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
    });
    const temp = await response.text();
    console.log(temp);

    try {
      const data = JSON.parse(temp);
      setGameState(data);
      setSessionData(data);
    } catch (error) {
      console.error("Error parsing JSON:", error);
    }
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
    const data = await response.json();
    setGameState(data);
    setSessionData(data);
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
    const data = await response.json();
    setGameState(data);
    setSessionData(data);
  }

  return (
    <div>
      {gameState && (
        <div>
          {playerActionButtons}
          {/* Render gameState or do whatever you need to do */}
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
