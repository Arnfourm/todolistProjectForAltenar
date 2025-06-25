import { useEffect, useState } from "react";
import axios from 'axios';
import '../styles/header.css';

function Header() {
    const [user, setUser] = useState(null);
    const userId = '65e411f2-6c49-4906-a217-bbd5529b8a24';

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