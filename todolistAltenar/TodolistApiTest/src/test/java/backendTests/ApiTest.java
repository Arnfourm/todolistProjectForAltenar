package backendTests;

import io.restassured.*;
import io.restassured.http.ContentType;
import io.restassured.response.Response;
import org.junit.jupiter.api.*;

import java.util.UUID;

import static org.junit.jupiter.api.Assertions.assertEquals;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
public class ApiTest {

    private static final String BASE_URL = System.getProperty("WEBAPP-URL") + ":" + System.getProperty("WEBAPP-PORT") + "/api";
    private static final String NOTE_ENDPOINT = "/Note";
    private static final String GROUP_ENDPOINT = "/Group";
    private static final String USER_ENDPOINT = "/User";
    private static final String NOTE_CONTENT_ENDPOINT = "/Note/Content";

    private static UUID userId;
    private static UUID groupId;
    private static UUID noteId;

    @BeforeEach
    public void setUp() {
        RestAssured.baseURI = BASE_URL;

        System.out.println(BASE_URL);
    }

    @Test
    @Order(1)
    public void testPostUser() {
        String newUserName = "Test_username";
        String newEmailAddress = "Test_Email";
        String newPassword = "Test_pass";

        String newUser = String.format("""
                {
                  "username": "%s",
                  "userEmail": "%s",
                  "userPassword": "%s"
                }
                """, newUserName, newEmailAddress, newPassword);

        Response response = RestAssured.given().contentType(ContentType.JSON).body(newUser).post(USER_ENDPOINT);
        userId = response.jsonPath().getUUID("idUser");

        System.out.println(userId);
    }

    @Test
    @Order(2)
    public void testPostGroup() {
        String newGroupTitle = "test_group_title";

        String newGroup = String.format("""
                {
                "userID": "%s",
                "titleGroup": "%s"
                }
                """, userId, newGroupTitle);

        Response response = RestAssured.given().contentType(ContentType.JSON).body(newGroup).post(GROUP_ENDPOINT);
        groupId = response.jsonPath().getUUID("idGroup");

        System.out.println(groupId);
    }

    @Test
    @Order(3)
    public void testPostNote() {
        String newNoteTitle = "test_note_title";
        String newNoteContent = "test_note_content";

        String newNote = String.format("""
                {
                "userID": "%s",
                "titleNote": "%s",
                "noteContent": "%s",
                "groupID": "%s"
                }
                """, userId, newNoteTitle, newNoteContent, groupId);

        Response responseNewNote = RestAssured.given().contentType(ContentType.JSON).body(newNote).post(NOTE_ENDPOINT);
        noteId = responseNewNote.jsonPath().getUUID("idNote");

        String newContentNote = String.format("""
                {
                "noteContent": "%s"
                }
                """, newNoteContent);

        RestAssured.given().contentType(ContentType.JSON).body(newContentNote).put(NOTE_CONTENT_ENDPOINT + "/" + noteId);

        System.out.println(noteId);
    }

    @Test
    @Order(4)
    public void testGetUser() {
        Response response = RestAssured.given().get(USER_ENDPOINT + "/ById/" + userId);

        assertEquals(200, response.statusCode(), "Fail to get a user");
        assertEquals("Test_username", response.jsonPath().getString("username"), "Username isn't correct");
        assertEquals("Test_Email", response.jsonPath().getString("userEmail"), "User email isn't correct");
        assertEquals("Test_pass", response.jsonPath().getString("userPassword"), "User password isn't correct");
    }

    @Test
    @Order(5)
    public void testGetGroup() {
        Response response = RestAssured.given().get(GROUP_ENDPOINT + "/ById/" + groupId);

        assertEquals(200, response.statusCode(), "Fail to get a group");
        assertEquals(userId, response.jsonPath().getUUID("userID"), "Group: fail to get a user id");
        assertEquals("test_group_title", response.jsonPath().getString("titleGroup"), "Group: fail to get a group title");
    }

    @Test
    @Order(6)
    public void testGetNote() {
        Response response = RestAssured.given().get(NOTE_ENDPOINT + "/ById/" + noteId);

        assertEquals(200, response.statusCode(), "Fail to get a note");
        assertEquals("test_note_title", response.jsonPath().getString("titleNote"), "Note: title note is incorrect");
        assertEquals("test_note_content", response.jsonPath().getString("noteContent"), "Note: note content is incorrect");
        assertEquals(groupId, response.jsonPath().getUUID("groupID"), "Note: group id is incorrect");
    }
}