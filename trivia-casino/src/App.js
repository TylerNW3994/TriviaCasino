import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import logo from './logo.svg';
import './App.css';
import Home from './components/Home';
import Games from './components/Games';

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
          {/* <Route path=":gameId" element={<GameDetail />} /> */}
      </Routes>
    </div>
  </Router>
  );
}

export default App;
