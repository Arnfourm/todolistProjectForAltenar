const modal = document.getElementById('modalWindow');
const closeModal = document.getElementById('closeModal');
const editInput = document.getElementById('editInput');

let currentNote = null;
let currentNoteElement = null;

function openModal(note) {
    modal.classList.remove('hidden');
    editInput.value = note.titleNote;
    currentNote = note;
}

closeModal.addEventListener('click', () => {
    modal.classList.add('hidden');
});

// saveBtn.addEventListener('click', () => {
//     const newText = editInput.value.trim();
//     if (newText !== '') {
//         currentNote.titleNote = newText;
//         currentNoteElement.textContent = newText;
//         modal.classList.add('hidden');
        
//         fetch(`http://localhost:5140/Note/${currentNote.noteID}`, {
//             method: 'PUT',
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify(currentNote)
//         });
        
//     }
// });