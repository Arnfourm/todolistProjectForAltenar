import { useEffect, useState } from "react";
import axios from 'axios';
import '../styles/header.css';
import { User } from "../interfaces/userInterface";

function Header() {
    const [user, setUser] = useState<User | null>(null);
    const userId: string = '13876536-8ce5-4673-84b5-a8f8efefc75f';

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