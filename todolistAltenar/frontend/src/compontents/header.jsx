import { useEffect, useState } from "react";
import axios from 'axios';
import '../styles/header.css';

function Header() {
    const [user, setUser] = useState(null);
    const userId = 'alsdkljhc';

    useEffect(() => {
        axios.get(`http://localhost:5140/User/ById/${userId}`)
            .then(res => setUser(res.data))
            .catch(error => console.error("Ошибка при получении пользователя: ", error))
    }, []);

    return (
        <header>
            <h1 class="headerName">To-do-List</h1>
            <div id="userContainer">
                {user ? (
                    <p class="userName">Current user: {user.username}</p>
                ) : (
                    <p class="userName">Loading user..</p>
                )}
            </div>
        </header>
    );
}

export default Header;