import React from 'react';
import Card from "../blackjackComponents/Card";

function BlackjackPlayer({ playerData, gameInSession = false }) {
    // console.log(JSON.stringify(playerData));

    const unknownCardData = {
        rank : "?",
        suit : "Hidden"
    }

    return (
        <div key={playerData.username}>
            <h3>{playerData.username}</h3>
            <p>Score: {(gameInSession && playerData.username === "Dealer") ? "???" : playerData.score}</p>
            <div>
            {playerData.hand.map((card, index) => {
                return <span key={index}>
                    {gameInSession && playerData.username === "Dealer" && index > 0 ? (
                        <Card cardData={unknownCardData} />
                    ) : (
                        <Card cardData={card} />
                    )}
                </span>
            })}
            </div>
            {playerData.chips != null && (
                <p>Chips: {playerData.chips}</p>
            )}
        </div>
    );
}

export default BlackjackPlayer;
