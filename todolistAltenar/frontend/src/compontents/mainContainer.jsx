import { useEffect, useState } from "react";
import axios from "axios";
import './modalWindow';
import ModalWindow from "./modalWindow";
import '../styles/main.css';

function MainContainer() {
    const [groups, SetGroups] = useState([]);
    const [notes, SetNotes] = useState([]);
    const [noteContent, SetNoteContent] = useState({});
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedNote, setSelectedNote] = useState(null);

    const userId = 'a8aea2f7-c7fb-48a5-863b-e4b7633f8c36';
    useEffect(() => {
        axios.get(`http://localhost:5140/Group/ByUserId/${userId}`)
            .then(res => SetGroups(res.data))
            .catch(error => console.log("Не удалось загрузить группы: ", error));
    }, [])


    useEffect(() => {
        axios.get(`http://localhost:5140/Note/ByUserId/${userId}`)
            .then(res => SetNotes(res.data))
            .catch(error => console.log("Не удалось загрузить заметки: ", error))
    }, [])

    useEffect(() => {
        if (notes.length === 0) return;

        notes.forEach(singleNote => {
            axios.get(`http://localhost:5140/Note/Content/${singleNote.noteID}`)
                .then(res => {
                    SetNoteContent(prev => ({
                        ...prev,
                        [singleNote.noteID]: res.data
                    }));
                })
                .catch(error => console.error("Не удалось получить контент", error));
        })
    }, [notes]);

    const handleNoteClick = (note) => {
        setSelectedNote(note);
        setIsModalOpen(true);
    };

    return (
        <main>
            <div id="noteContainer">
                {groups.map(singleGroup => (
                    <div key={singleGroup.idGroup} className="groupDiv">
                        <p className="groupName">⇩ {singleGroup.titleGroup}</p>
                        {notes.filter(singleNote => singleNote.groupID === singleGroup.idGroup).map(singleNote => (
                            <div key={singleNote.noteID} className="noteDiv" onClick={() => handleNoteClick(singleNote)}>
                                <p className="noteName">{singleNote.titleNote}</p>
                            </div>
                        ))}
                    </div>
                ))}
            </div>

            <ModalWindow
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                note={selectedNote}
                content={selectedNote ? noteContent[selectedNote.noteID] : null}
            />
        </main>
    );
}

export default MainContainer;