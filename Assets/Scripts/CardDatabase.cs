using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new();

    public void Awake()
    {
        cardList.Add(new Card(0, "Among Us", "Attack", 6, "Images/Card/Amongus"));
        cardList.Add(new Card(1, "My Reaction To That Misinformation", "Attack", 7, "Images/Card/MyReactionToThatMisinformation"));
        cardList.Add(new Card(2, "Bing Chilling", "Attack", 9, "Images/Card/BingChilling"));
        cardList.Add(new Card(3, "Slap", "Attack", 6, "Images/Card/Slap"));
        cardList.Add(new Card(4, "The Rock", "Attack", 9, "Images/Card/TheRock"));
        cardList.Add(new Card(5, "Dream", "Attack", 6, "Images/Card/Dream"));
        cardList.Add(new Card(6, "Death Treats", "Attack", 7, "Images/Card/DeathTreats"));
        cardList.Add(new Card(7, "Blue Lobster", "Attack", 7, "Images/Card/BlueLobster"));

        cardList.Add(new Card(8, "Mr Incredible Canny", "Health", 3, "Images/Card/MrIncredibleCanny"));
        cardList.Add(new Card(9, "Left Zoolander", "Health", 3, "Images/Card/LeftZoolander"));
        cardList.Add(new Card(10, "Social Credit", "Health", 15, "Images/Card/SocialCredit"));
        cardList.Add(new Card(11, "Saul 3D", "Health", 4, "Images/Card/Saul3D"));
        cardList.Add(new Card(12, "Wise Mystical Tree", "Health", 5, "Images/Card/WiseMysticalTree"));
        cardList.Add(new Card(13, "Chestnuts Roasting On An Open Fire", "Health", 5, "Images/Card/ChestnutsRoastingOnAnOpenFire"));
        cardList.Add(new Card(14, "Average Enjoyer", "Health", 4, "Images/Card/AverageEnjoyer"));
        cardList.Add(new Card(15, "Right Zoo lander", "Health", 3, "Images/Card/RightZoolander"));

        cardList.Add(new Card(16, "Belly Dance", "Defense", 2, "Images/Card/BellyDance"));
        cardList.Add(new Card(17, "My Reaction To That Information", "Defense", 3, "Images/Card/MyReactionToThatInformation"));
        cardList.Add(new Card(18, "Juan", "Defense", 3, "Images/Card/Juan"));
        cardList.Add(new Card(19, "Uncle Apple", "Defense", 4, "Images/Card/UncleApple"));
        cardList.Add(new Card(20, "Think Mark", "Defense", 4, "Images/Card/ThinkMark"));
        cardList.Add(new Card(21, "Trust", "Defense", 5, "Images/Card/Trust"));
        cardList.Add(new Card(22, "The One Piece Is Real", "Defense", 3, "Images/Card/TheOnePieceIsReal"));
        cardList.Add(new Card(23, "Andrew Tate", "Defense", 4, "Images/CardAndrewTate"));

        cardList.Add(new Card(24, "Sparks Joy", "Powerup", 4, "Images/Card/SparksJoy"));
        cardList.Add(new Card(25, "Gus", "Powerup", 5, "Images/Card/Gus"));
        cardList.Add(new Card(26, "Water White : Chemistry", "Powerup", 6, "Images/Card/WaterWhiteChemistry"));
        cardList.Add(new Card(27, "Four", "Powerup", 4, "Images/Card/Four"));
        cardList.Add(new Card(28, "Sword", "Powerup", 4, "Images/Card/Sword"));
        cardList.Add(new Card(29, "Mr Beast (?)", "Powerup", 4, "Images/Card/MrBeast"));
        cardList.Add(new Card(30, "Mixue", "Powerup", 4, "Images/Card/Mixue"));
        cardList.Add(new Card(31, "Friendship", "Powerup", 4, "Images/Card/Friendship"));
        Debug.Log("hi");
    }
}
