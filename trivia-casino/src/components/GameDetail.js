import { useParams } from 'react-router-dom';

function GameDetail() {
  const { gameId } = useParams();

  return (
    <div>
      <h2>Game Detail: {gameId}</h2>
      <p>How to Play Game:</p>
    </div>
  );
}

export default GameDetail;