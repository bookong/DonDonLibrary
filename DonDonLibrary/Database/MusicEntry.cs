namespace DonDonLibrary.Database
{
    // I would implement entry reading inside a MusicEntry class
    // but the names are separate from the main entry so it would just be an overcomplicated code
    public struct MusicEntry
    {
        public string name;
        public int offset;
        public int previewStart;
    }
}
