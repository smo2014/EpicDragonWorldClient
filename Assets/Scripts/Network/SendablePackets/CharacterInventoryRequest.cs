public class CharacterInventoryRequest : SendablePacket
{
    public CharacterInventoryRequest(string characterName)
    {
        WriteShort(15); // Packet id.
        WriteString(characterName);
    }
}
