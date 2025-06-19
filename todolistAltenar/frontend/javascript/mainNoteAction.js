'use strict'

async function fetchNote() {
    try {
        const userId = '5eaa9aeb-390c-46d7-9ca8-bbd5a76ce74d';
        
        const responseGroups = await fetch(`http://localhost:5140/Group/${userId}/GetGroupByUserId`);
        const allGroups = await responseGroups.json();

        const responseNotes = await fetch(`http://localhost:5140/Note/${userId}/GetNoteByUserId`);
        let allNotes = await responseNotes.json();
        
        const notesContainer = document.getElementById('noteContainer');

        allGroups.forEach(singleGroup => {
            const groupShow = document.createElement('div');
            groupShow.classList.add('groupDiv');

            const groupNameShow = document.createElement('p');
            groupNameShow.textContent = "⇩ " + singleGroup.titleGroup;
            groupNameShow.classList.add('groupName');

            notesContainer.appendChild(groupShow);
            groupShow.appendChild(groupNameShow);

            allNotes.forEach(singleNote => {
                if (singleNote.groupID === singleGroup.idGroup){
                    const noteShow = document.createElement('div');
                    noteShow.classList.add('noteDiv');
                    
                    const noteNameShow = document.createElement('p');
                    noteNameShow.textContent = singleNote.titleNote;
                    noteNameShow.classList.add('noteName')

                    noteShow.addEventListener('click', () => {
                        openModal(singleNote);
                    });

                    groupShow.appendChild(noteShow);
                    noteShow.appendChild(noteNameShow);
                    
                    allNotes = allNotes.filter(note => note != singleNote);
                }
            });
        });
    } catch (error) {
        console.error('ошибка', error);
    }
}

fetchNote();