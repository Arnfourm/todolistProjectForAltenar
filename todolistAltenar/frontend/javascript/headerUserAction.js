'use strict'

async function fetchUser() {
    try {
        const userId = '5eaa9aeb-390c-46d7-9ca8-bbd5a76ce74d';

        const response = await fetch(`http://localhost:5140/User/ById/${userId}`);
        const currentUser = await response.json();

        const userContainer = document.getElementById('userContainer');
        
        const usermameShow = document.createElement('p');
        usermameShow.classList.add('userName');
        usermameShow.textContent = `Current user: ${currentUser.username}`
        
        userContainer.appendChild(usermameShow);
    } catch (error) {
        console.error('ошибка', error);
    }
}

fetchUser();