import { useEffect, useState } from "react";
import axios from "axios";
import '../styles/modalWindow.css'

function ModalWindow({ isOpen, onClose, note }) {
    const [titleNote, SetTitle] = useState('');
    const [contentNote, SetContent] = useState('');

    useEffect(() => {
        if (note) {
            SetTitle(note.titleNote);
            SetContent(note.noteContent);
        }
    }, [note]);

    if (!isOpen) return null;

    return (
        <div className="modal">
            <div className="modalContent">
                <span className="close" onClick={onClose}>&times;</span>
                <input type="text" className="nameNoteInput" value={titleNote} onChange={(e) => SetTitle(e.target.value)} />
                <textarea className="editInput" value={contentNote} onChange={(e) => SetContent(e.target.value)} />
                <section className="modalButtons">
                    <button onClick={() => {
                        SetTitle(note.titleNote);
                        SetContent(note.noteContent);
                    }}>Отмена</button>
                    <button onClick={() => {
                        if (note.titleNote !== titleNote) {
                            const newTitleRequest = {
                                titleNote: titleNote,
                                groupID: note.groupID
                            }
                            axios.put(`http://localhost:5140/Note/${note.noteID}`, newTitleRequest)
                                .then((data) => {
                                    console.log(data);
                                    note.titleNote = titleNote;
                                })
                                .catch(error => ("Не удалось изменить название", error));
                        };

                        if (note.noteContent !== contentNote) {
                            const newContentRequest = {
                                noteContent: contentNote
                            };
                            axios.put(`http://localhost:5140/Note/Content/${note.noteID}`, newContentRequest)
                                .then((data) => {
                                    console.log(data);
                                    note.noteContent = contentNote;
                                })
                                .catch(error => ("Не удалось изменить контент", error));
                        };
                    }}>Сохранить</button>
                </section>
            </div>
        </div>
    );
}

export default ModalWindow;