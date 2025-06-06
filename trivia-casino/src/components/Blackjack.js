import React, { useState } from "react";
import { useGameSession } from "./GameSessionProvider";
import { useUser } from './UserProvider';
import BlackjackPlayer from "../blackjackComponents/BlackjackPlayer";

export default function Blackjack() {
  let defaultGameState = {
    gameType: "Blackjack",
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
        <span>Bet: </span>
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
      GameState: {
        GameId: "",
        GameType: "Blackjack",
        DealerDTO: {},
        PlayerDTOs: [
          {
            Username: user?.username,
            Bet: betAmount
          }
        ]
      },
      Player: user
    };

    // console.log(JSON.stringify(payload));

    const response = await fetch("/api/blackjack/newgame", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await response.json();
    processData(data);
  }

  /**
   * Sanitizes the bet amount by ensuring it does not exceed the user's available chips.
   * If the bet is greater than the user's chips, it returns the maximum allowable bet.
   * Otherwise, it returns the absolute integer value of the bet.
   *
   * @param {number} bet - The proposed bet amount.
   * @return {number} - The sanitized bet amount, adjusted to not exceed the user's chips.
   */
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
    
    // console.log(JSON.stringify(payload));

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
      // console.log(JSON.stringify(data));
      var gameData = data.data;
      
      setGameState(gameData);
      setMessage(gameData.message);
      setSessionData(gameData);

      setDealerData({
        score : gameData.dealerDTO.score,
        hand : gameData.dealerDTO.hand,
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
          {gameState.playerDTOs && (
            <div>
              <BlackjackPlayer key="dealer" playerData={dealerData} gameInSession={gameInSession} />
              {Object.entries(gameState.playerDTOs).map(([username, player]) => {
                let playerData = {
                  score : player.score,
                  hand : player.hand,
                  username : player.username,
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
