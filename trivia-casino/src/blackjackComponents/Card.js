import React from 'react';
import './Card.css';

const suitData = {
    Spade:   { symbol: '♠', color: 'black' },
    Club:    { symbol: '♣', color: 'black' },
    Diamond: { symbol: '♦', color: 'red' },
    Heart:   { symbol: '♥', color: 'red' }
};

function Card({ cardData }) {
    const { symbol, color } = suitData[cardData.suit];

    return (
        <div className={`card ${color}`}>
            <div className="corner top-left">
                {cardData.rank} <br /> {symbol}
            </div>
            <div className="middle">{symbol}</div>
            <div className="corner bottom-right">
                {cardData.rank} <br /> {symbol}
            </div>
            
        </div>
    );
}

export default Card;
