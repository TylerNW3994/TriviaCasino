import React from 'react';
import { useUser } from './UserProvider';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const { login } = useUser();
    const navigate = useNavigate();

    function loginAsGuest() {
        let randomIdentifier = Math.floor(Math.random() * 10000);
        const guestUserData = {
            token: 'guestToken',
            currentPlayer: {
                userId: 0,
                username: 'Guest' + randomIdentifier,
                email: '',
                gamesWon: 0,
                gamesPlayed: 0,
                chips: 1000
            }
        }

        login(guestUserData);

        navigate('/Games');
    }

    return (
        <div>
            <h2>Login</h2>
            <button onClick={ loginAsGuest }>Login as Guest</button>
        </div>
    );
};

export default Login;
