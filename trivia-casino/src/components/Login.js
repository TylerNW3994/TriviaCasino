import { useState } from 'react';
import { useUser } from './UserProvider';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [registerEmail, setRegisterEmail] = useState('');
    const [registerPassword, setRegisterPassword] = useState('');
    const [registerConfirmPassword, setRegisterConfirmPassword] = useState('');
    const [registerDisplayName, setRegisterDisplayName] = useState('');
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

    async function testLaravelGet(email) {
        console.log('getting res');
        const res = await fetch(`http://localhost:8000/api/player/getUser/${email}`);
        console.log('getting data');
        const data = await res.json();

        console.log(data);
    }

    async function testLaravelCreate(userId, displayName) {
        const res = await fetch(`http://localhost:8000/api/player/createPlayer`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify({
            user_id: userId,
            display_name: displayName,
            }),
        });

        const data = await res.json();
        console.log(data);
    }

    return (
        <div>
            <div className="row">
                <div className="col-md-6">
                    <h2>Login</h2>
                    <form>
                    <label>
                        Email:
                        <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        />
                    </label>
                    <br />
                    <label>
                        Password:
                        <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        />
                    </label>
                    <br />
                    <button type="submit">Login</button>
                    </form>
                </div>
                <div className="col-md-6">
                    <h2>Register</h2>
                    <form>
                    <label>
                        Email:
                        <input
                        type="email"
                        value={registerEmail}
                        onChange={(e) => setRegisterEmail(e.target.value)}
                        />
                    </label>
                    <br />
                    <label>
                        Password:
                        <input
                        type="password"
                        value={registerPassword}
                        onChange={(e) => setRegisterPassword(e.target.value)}
                        />
                    </label>
                    <br />
                    <label>
                        Confirm Password:
                        <input
                        type="password"
                        value={registerConfirmPassword}
                        onChange={(e) => setRegisterConfirmPassword(e.target.value)}
                        />
                    </label>
                    <br />
                    <label>
                        Display Name:
                        <input
                        type="text"
                        value={registerDisplayName}
                        onChange={(e) => setRegisterDisplayName(e.target.value)}
                        />
                    </label>
                    <br />
                    <button type="submit">Register</button>
                    </form>
                </div>
            </div>

            <button onClick={ loginAsGuest }>Login as Guest</button>
            <button onClick={ () => testLaravelGet(email) }>Test Get</button>
            <button onClick={ testLaravelCreate }>Test Create</button>
        </div>
    );
};

export default Login;
