import { useParams } from 'react-router-dom';

function GameDetail() {
  const { gameName } = useParams();

  return (
    <div>
      <h2>{gameName.toUpperCase()}</h2>
      <button>Learn how to play {gameName}</button>
    </div>

    // Implement games list here
    // Create new Game button
  );
}

export default GameDetail;