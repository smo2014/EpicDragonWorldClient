public class CharacterInventoryRequest : SendablePacket
{
    public CharacterInventoryRequest(string characterName)
    {
        WriteShort(14); // Packet id.
        WriteString(characterName);
    }
}
