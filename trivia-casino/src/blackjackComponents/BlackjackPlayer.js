import React from 'react';
import Card from "../blackjackComponents/Card";

function BlackjackPlayer({ playerData }) {
    // console.log(JSON.stringify(playerData));

    return (
        <div key={playerData.username}>
            <h3>{playerData.username}</h3>
            <p>Score: {playerData.score}</p>
            <div>
            {playerData.hand.map((card) => {
                return <Card cardData={card} />
            })}
            </div>
            {playerData.chips != null && (
                <p>Chips: {playerData.chips}</p>
            )}
        </div>
    );
}

export default BlackjackPlayer;
