using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using Places_Service.Dtos;

namespace Places_Service.Async;

public class GetPlaceByIdHandler
{
    [Obsolete("Obsolete")]
    public void HandleMessage(byte[] body)
    {
        // Створіть потік для читання з байтового масиву
        using (MemoryStream memoryStream = new MemoryStream(body))
        {
            // Створіть BinaryFormatter для десеріалізації
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Десеріалізуйте байтовий масив у об'єкт PlaceDto
            PlaceDto placeDto = (PlaceDto)binaryFormatter.Deserialize(memoryStream);

            // Тепер ви можете використовувати об'єкт placeDto
            
            
        }
    }
}