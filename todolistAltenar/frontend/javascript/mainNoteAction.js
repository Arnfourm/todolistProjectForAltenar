'use strict'

async function fetchNote() {
    try {
        const userId = '5eaa9aeb-390c-46d7-9ca8-bbd5a76ce74d';
        
        const responseGroups = await fetch(`http://localhost:5140/Group/${userId}/GetGroupByUserId`);
        const allGroups = await responseGroups.json();

        const responseNotes = await fetch('http://localhost:5140/Note');
        const allNotes = await responseNotes.json();

        const notesContainer = document.getElementById('noteContainer');

        allGroups.forEach(singleGroup => {
            const groupShow = document.createElement('div');
            groupShow.classList.add('groupDiv');
            notesContainer.appendChild(groupShow);
        });
    } catch (error) {
        console.error('ошибка', error);
    }
}

fetchNote();