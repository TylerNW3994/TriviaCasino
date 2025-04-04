import React, { useState } from "react";
import { useGameSession } from "./GameSessionProvider";
import { useUser } from './UserProvider';
import BlackjackPlayer from "../blackjackComponents/BlackjackPlayer";

export default function Blackjack() {
  let defaultGameState = {
    gameId: "",
    currentPlayer: "",
    dealerHand: [],
    players: []
  };

  const { user } = useUser();
  const [gameState, setGameState] = useState(defaultGameState);
  const [dealerData, setDealerData] = useState({
    playerScore: 0,
    playerHand: [],
    username: "Dealer",
    chips: null
  });
  const [message, setMessage] = useState("Welcome " + user?.username);
  const { setSessionData } = useGameSession();
  let gameInSession = gameState?.currentPlayer !== "";
  const [betAmount, setBetAmount] = useState(0);

  React.useEffect(() => {}, [gameState]);

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
        <input
          type="number"
          placeholder="Enter your bet"
          value={betAmount}
          onChange={(e) => setBetAmount(sanitizeBetAmount(e.target.value))}
        />
        <button onClick={startNewGame}>New Game</button>
      </>
    );
  }

  async function startNewGame() {
    user.Bet = betAmount;
    const payload = {
      Username: user?.username,
      GameState: gameState,
      Player: user
    };

    const response = await fetch("/api/blackjack/newgame", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  function sanitizeBetAmount(bet) {
    if (bet > user.chips) {
      return user.chips;
    } else {
      return Math.abs(parseInt(bet));
    }
  }

  async function handleHit(username) {
    const payload = {
      Username: user?.username,
      GameState: gameState,
      Player: user,
    };

    const response = await fetch("/api/blackjack/hit", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  async function handleStand(username) {
    const payload = {
      Username: user?.username,
      GameState: gameState,
      Player: user,
    };

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
      setGameState(data.gameData);
      setMessage(data.gameData.message);
      setSessionData(data.gameData);

      setDealerData({
        playerScore : data.gameData.dealerScore,
        playerHand : data.gameData.dealerHand,
        username : "Dealer",
        chips : null
      });
    } catch (error) {
      console.error("Error parsing JSON:", error);
    }
  }

  return (
    <div>
      {gameState && (
        <div>
          {message}
          <br></br>
          {playerActionButtons}
          {gameState.playerDatas && (
            <div>
              <BlackjackPlayer key="dealer" playerData={dealerData} />
              {Object.entries(gameState.playerDatas).map(([username, player]) => {
                let playerData = {
                  playerScore : player.score,
                  playerHand : player.hand,
                  username : username,
                  chips : player.chips
                };

                return <BlackjackPlayer key={username} playerData={playerData} />
              })}
            </div>
          )}
        </div>
      )}
    </div>
  );
}
