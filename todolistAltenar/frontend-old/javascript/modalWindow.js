'use strict'

const modal = document.getElementById('modalWindow');
const closeModal = document.getElementById('closeModal');
const nameNote = document.getElementById('nameNoteInput');
const editArea = document.getElementById('editInput');
const editButton = document.getElementById('acceptEdit');
const cancelButton = document.getElementById('cancelEdit');

const userId = '5eaa9aeb-390c-46d7-9ca8-bbd5a76ce74d';

let currentNote = null;
let currentNoteContent = null;
let currentNoteTitleShow = null;

function openModal(note, content, noteNameShow) {
    modal.classList.remove('hidden');
    nameNote.value = note.titleNote;
    editArea.value = content.noteContent;
    currentNote = note;
    currentNoteContent = content;
    currentNoteTitleShow = noteNameShow;
}

closeModal.addEventListener('click', () => {
    modal.classList.add('hidden');
});

editButton.addEventListener('click', async () => {
    const newNoteName = nameNote.value.trim();
    const newContent = editArea.value.trim();

    if (newNoteName !== '' && newNoteName !== currentNote.titleNote){
        currentNote.titleNote = newNoteName;

        await fetch(`http://localhost:5140/Note/${currentNote.noteID}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({userID: userId, titleNote: currentNote.titleNote, groupID: currentNote.groupID})
        })
        .then(data => {
            currentNoteTitleShow.textContent = currentNote.titleNote;

            console.log("Название обновлено:", data);
        })
        .catch(error => {
            console.error("Ошибка:", error);
        });
    }

    if (newContent !== currentNoteContent.noteContent){
        currentNoteContent.noteContent = newContent;

        await fetch(`http://localhost:5140/Note/Content/${currentNote.noteID}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({noteContent: currentNoteContent.noteContent})
        })
        .then(data => {
            console.log("Контент обновлен:", data);
        })
        .catch(error => {
            console.error("Ошибка:", error);
        });
    }
});

cancelButton.addEventListener('click', () => {
    
    nameNote.value = currentNote.titleNote;
    editArea.value = currentNoteContent.noteContent;
});