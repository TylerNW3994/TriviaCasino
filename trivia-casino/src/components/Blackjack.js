import React, { useState } from 'react';

export default function Blackjack() {
  const [gameState, setGameState] = useState(null);

  async function startNewGame() {
    const response = await fetch('/api/blackjack/newgame', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
    });
    const data = await response.json();
    setGameState(data);
  }

  async function handleHit() {
    const response = await fetch('/api/blackjack/hit', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(gameState),
    });
    const data = await response.json();
    setGameState(data);
  }

  return (
    <div>
      <button onClick={startNewGame}>New Game</button>
      <button onClick={handleHit}>Hit</button>
      {/* Render gameState or do whatever you need to do */}
      {gameState.Players && (
        <div>
          <h2>Dealer's Hand:</h2> 
          <p>{JSON.stringify(gameState.dealerHand)}</p>
          {Object.entries(gameState.Players).map((player) => (
            <div key={player.Name}>
              <h3>{player.Name}</h3>
              <p>{player.Score}</p>
              <div>
                Hand: {player.hand && player.hand.join(', ')}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}