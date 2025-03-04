import React from 'react';
import { useParams } from 'react-router-dom';
import Blackjack from './Blackjack';

const gameComponents = {
  blackjack: Blackjack,
};

function Play() {
  const { gameName } = useParams();
  const normalizedGameName = gameName.toLowerCase();
  const GameComponent = gameComponents[normalizedGameName];

  return (
    <div>
      <h1>Playing {gameName.toUpperCase()}</h1>
      <GameComponent />
    </div>
  );
}

export default Play;
