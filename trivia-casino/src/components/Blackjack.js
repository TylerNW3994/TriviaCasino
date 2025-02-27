import { useParams } from 'react-router-dom';

function Blackjack () {
  const { gameId } = useParams();

  return (
    <div>
      <h2>Add Blackjack functionality here</h2>
    </div>
  );
}

export default Blackjack;