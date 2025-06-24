import { useEffect, useState } from "react";
import axios from "axios";
import '../styles/modalWindow.css'

function ModalWindow({ isOpen, onClose, note, content }) {
    const [titleNote, SetTitle] = useState('');
    const [contentNote, SetContent] = useState('');

    useEffect(() => {
        if (note && content) {
            SetTitle(note.titleNote);
            SetContent(content.noteContent);
        }
    }, [note, content]);

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
                        SetContent(content.noteContent);
                    }}>Отмена</button>
                    <button onClick={() => {
                        if (note.titleNote !== titleNote) {
                            const newTitleRequest = {
                                userID: note.userID,
                                titleNote: titleNote,
                                groupID: note.groupID
                            }
                            axios.put(`http://localhost:5140/Note/${note.noteID}`, newTitleRequest)
                                .then(() => {
                                    note.titleNote = titleNote;
                                })
                                .catch(error => ("Не удалось изменить название", error));
                        };

                        if (content.noteContent !== contentNote) {
                            const newContentRequest = {
                                noteContent: contentNote
                            };
                            axios.put(`http://localhost:5140/Note/Content/${note.noteID}`, newContentRequest)
                                .then(() => {
                                    content.noteContent = contentNote;
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