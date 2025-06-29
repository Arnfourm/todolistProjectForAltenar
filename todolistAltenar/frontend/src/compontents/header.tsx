import { useEffect, useState } from "react";
import axios from 'axios';
import '../styles/header.css';
import { User } from "../interfaces/userInterface";

function Header() {
    const [user, setUser] = useState<User | null>(null);
    const userId:string = 'd3372125-9036-4060-86f2-728e1c4b0ef3';

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