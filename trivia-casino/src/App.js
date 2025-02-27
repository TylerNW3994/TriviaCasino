import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import Home from './components/Home';
import Games from './components/Games';
import GameDetail from './components/GameDetail';
import HowToPlay from './components/HowToPlay';

function App() {
  return (
    <Router>
    <div className="App">
      <nav className="App-nav">
        <Link to="/">Home</Link>
        <Link to="/games">Games</Link>
      </nav>

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/games" element={<Games />} />
        <Route path="/games/:gameName" element={<GameDetail />} />
        <Route path="/howtoplay/:gameName" element={<HowToPlay />} />
      </Routes>
    </div>
  </Router>
  );
}

export default App;
