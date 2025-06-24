import { useEffect, useState } from "react";
import axios from "axios";
import '../styles/modalWindow.css'

function ModalWindow({ isOpen, onClose, note, content }) {
    if (!isOpen || !note || !content) return null;

    return (
        <div className="modal">
            <div className="modalContent">
                <span className="close" onClick={onClose}>&times;</span>
                <input type="text" className="nameNoteInput" defaultValue={note.titleNote} />
                <textarea className="editInput" defaultValue={content.noteContent}></textarea>
                <section className="modalButtons">
                    <button>Отмена</button>
                    <button>Сохранить</button>
                </section>
            </div>
        </div>
    );
}

export default ModalWindow;