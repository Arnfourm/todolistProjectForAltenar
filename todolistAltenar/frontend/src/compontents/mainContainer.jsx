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

    const userId = '65e411f2-6c49-4906-a217-bbd5529b8a24';
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
                        <div className="topOfGroup">
                            <p className="groupName"><span>⇩</span> <input type="text" defaultValue={singleGroup.titleGroup} className="groupNameInput" id={`groupTitle-${singleGroup.idGroup}`} /></p>
                            <button className="topButtons" onClick={() => {
                                const newTitle = document.getElementById(`groupTitle-${singleGroup.idGroup}`).value;

                                const newTitleGroupItem = {
                                    userID: userId,
                                    titleGroup: newTitle
                                }
                                axios.put(`http://localhost:5140/Group/${singleGroup.idGroup}`, newTitleGroupItem)
                                    .then(() => {
                                        const prevGroups = [...groups];
                                        prevGroups.map(group => group.idGroup === singleGroup.idGroup ? { groupId: singleGroup.idGroup, titleGroup: newTitle } : group);

                                        SetGroups(prevGroups);
                                    })
                                    .catch(error => console.log("Не удалось обновить название группы", error));
                            }}>✎</button>
                            <button className="topButtons" onClick={() => {
                                axios.delete(`http://localhost:5140/Group/${singleGroup.idGroup}`)
                                    .then((data) => {
                                        SetGroups(groups.filter(group => group.idGroup !== singleGroup.idGroup))
                                    })
                                    .catch(error => console.log("На удалось удалить группу", error));
                            }}>✘</button>
                        </div>
                        {notes.filter(singleNote => singleNote.groupID === singleGroup.idGroup).map(singleNote => (
                            <div key={singleNote.noteID} className="noteDiv" onClick={() => handleNoteClick(singleNote)}>
                                <div className="taskDoneCircle" onClick={(event) => {
                                    event.stopPropagation();
                                    axios.delete(`http://localhost:5140/Note/${singleNote.noteID}`)
                                        .then(() => {
                                            const updatedNoteContent = { ...noteContent };
                                            delete updatedNoteContent[singleNote.noteStorageID];
                                            SetNoteContent(updatedNoteContent);

                                            SetNotes(notes.filter(note => note.noteID !== singleNote.noteID));
                                        })
                                        .catch(error => console.log("Не удалось удалить таск", error));
                                }}> </div>
                                <p className="noteName">{singleNote.titleNote}</p>
                            </div>
                        ))}

                        <div className="createNewNote" onClick={() => {
                            const newTaskRequest = {
                                userID: userId,
                                titleNote: "New task",
                                groupID: singleGroup.idGroup
                            };
                            axios.post(`http://localhost:5140/Note`, newTaskRequest)
                                .then((data) => {
                                    const newTaskItem = {
                                        idNote: data.data.idNote,
                                        userID: data.data.user.idUser,
                                        titleNote: data.data.titleNote,
                                        noteStorageID: data.data.noteStorage.idNoteStorage,
                                        groupID: data.data.noteGroup.idGroup
                                    };
                                    SetNotes(prevTasks => [...prevTasks, newTaskItem]);
                                })
                                .catch(error => console.log("Не удалось создать новый таск", error));
                        }}>
                            <p> + Добавить задачу </p>
                        </div>
                    </div>
                ))}

                <div className="createNewGroup">
                    <p className="newGroupName" onClick={() => {
                        const newGroupRequest = {
                            userID: userId,
                            titleGroup: "Новая группа"
                        };
                        axios.post(`http://localhost:5140/Group`, newGroupRequest)
                            .then((data) => {
                                console.log(data);
                                const newGroupItem = {
                                    idGroup: data.data.idGroup,
                                    userID: userId,
                                    titleGroup: data.data.titleGroup
                                }
                                SetGroups(prevGroups => [...prevGroups, newGroupItem]);
                            })
                            .catch(error => console.log("Не удалось создать группу", error));
                    }}> + Добавить группу </p>
                </div>
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