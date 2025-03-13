import React from 'react';
import Card from "../blackjackComponents/Card";

function BlackjackPlayer({ playerData }) {
    return (
        <div>
            <h3>{playerData.username}</h3>
            <p>Score: {playerData.playerScore}</p>
            <div>
            {playerData.playerHand.map((card) => {
                let cardData = {
                    rank : card.rank,
                    suit : card.suit
                }
                return <Card cardData={cardData} />
            })}
            </div>
            <p>Chips: {playerData.chips}</p>
        </div>
    );
}

export default BlackjackPlayer;
