namespace AdminPanel.Helper
{
	public class PictureSettings
	{
		public static string UploadFile( IFormFile File , string FolderName)
		{
			//1- Get folder path 

			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images" ,FolderName); 

			//2- Get File Path

			var FilePath =  Path.Combine(FolderPath, File.Name);

			// 3- Seve as File Streem 

			var Fs = new FileStream(FilePath , FileMode.Create);

			//4- copy file info streem 

			File.CopyTo(Fs);

			//5- retutn file name 

			return Path.Combine("images\\products",File.Name);
		}

		// function to Delete 

		public static void DeleteFile( string FolderName , string FileName)
		{
			var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FolderName , FileName); 

			if(File.Exists(FilePath))
			{
				File.Delete(FilePath);
			}

		}
	}
}
