import { useEffect, useState } from "react";
import axios from "axios";
import './modalWindow';
import ModalWindow from "./modalWindow";
import '../styles/main.css';
import { Group } from "../interfaces/groupInterface"
import { Note } from "../interfaces/noteInterface"

function MainContainer() {
    const [groups, SetGroups] = useState<Group[]>([]);
    const [notes, SetNotes] = useState<Note[]>([]);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [selectedNote, setSelectedNote] = useState<Note | null>(null);

    const userId: string = import.meta.env.VITE_USERID;
    const backAddr: string = import.meta.env.VITE_BACKEND_ADDRESS;

    useEffect(() => {
        axios.get(`${backAddr}/Group/ByUserId/${userId}`)
            .then(res => SetGroups(res.data))
            .catch(error => console.log("Не удалось загрузить группы: ", error));
    }, [])

    useEffect(() => {
        axios.get(`${backAddr}/Note/ByUserId/${userId}`)
            .then(res => SetNotes(res.data))
            .catch(error => console.log("Не удалось загрузить заметки: ", error))
    }, [])

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
                                const newTitle = (document.getElementById(`groupTitle-${singleGroup.idGroup}`) as HTMLInputElement).value;

                                const newTitleGroupItem = {
                                    userID: userId,
                                    titleGroup: newTitle
                                }
                                axios.put(`${backAddr}/Group/${singleGroup.idGroup}`, newTitleGroupItem)
                                    .then(() => {
                                        const prevGroups = [...groups];
                                        prevGroups.map(group => group.idGroup === singleGroup.idGroup ? { groupId: singleGroup.idGroup, titleGroup: newTitle } : group);

                                        SetGroups(prevGroups);
                                    })
                                    .catch(error => console.log("Не удалось обновить название группы", error));
                            }}>✎</button>
                            <button className="topButtons" onClick={() => {
                                axios.delete(`${backAddr}/Group/${singleGroup.idGroup}`)
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
                                    axios.delete(`${backAddr}/Note/${singleNote.noteID}`)
                                        .then(() => {
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
                                noteContent: "",
                                groupID: singleGroup.idGroup
                            };
                            axios.post(`${backAddr}/Note`, newTaskRequest)
                                .then((data) => {
                                    const newTaskItem = {
                                        noteID: data.data.idNote,
                                        userID: data.data.user.idUser,
                                        titleNote: data.data.titleNote,
                                        noteContent: data.data.contentNote,
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
                        axios.post(`${backAddr}/Group`, newGroupRequest)
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
            />
        </main>
    );
}

export default MainContainer;