using System.ComponentModel.DataAnnotations;

namespace Media.Models.Entity
{
    public class File
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateAt { get; set; }
        public string? Location { get; set; }
        public string Format { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public int MyProperty { get; set; }
        public int FileTypeId { get; set; }
        public virtual FileType FileType { get; set; }

        public int FolderId { get; set; }
        public virtual Folder Folder { get; set; }

    }
    public class FileType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<File> Files { get; set; }
    }
    public class Folder
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ورود عنوان الزامی است")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid CreatorId { get; set; }
        public int? ParentId { get; set; }
        public virtual Folder? Parent { get; set; }
        public virtual ICollection<Folder>? Childs { get; set; }
        public virtual List<File>? Files { get; set; }

    }
}
