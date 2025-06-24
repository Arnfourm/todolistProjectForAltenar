import { useEffect, useState } from "react";
import axios from 'axios';
import '../styles/header.css';

function Header() {
    const [user, setUser] = useState(null);
    const userId = 'a8aea2f7-c7fb-48a5-863b-e4b7633f8c36';

    useEffect(() => {
        axios.get(`http://localhost:5140/User/ById/${userId}`)
            .then(res => setUser(res.data))
            .catch(error => console.error("Ошибка при получении пользователя: ", error))
    }, []);

    return (
        <header>
            <h1 className="headerName">To-do-List</h1>
            <div id="userContainer">
                {user ? (
                    <p className="userName">Current user: {user.username}</p>
                ) : (
                    <p className="userName">Loading user..</p>
                )}
            </div>
        </header>
    );
}

export default Header;