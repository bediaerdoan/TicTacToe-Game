using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKodu : MonoBehaviour
{
    public Camera camera;
  
    public Material red;
    public Material yellow;

	private int[] squares;
    private Vector3[] xSignPositions;
    private Vector3[] oSignPositions;
    private int playerOrder; // 1 -> x,  2 -> 0
    private int xOrder; // x şekillerinin sırası
    private int oOrder; // o şekillerinin sırası
    private int moveCount;
    private GameObject square;


    void Start()
    {
        xSignPositions = new Vector3[5];
        oSignPositions = new Vector3[5];
        squares= new int[]{0,0,0,0,0,0,0,0,0};
        playerOrder = 1;
        xOrder = 1;
        oOrder = 1;
        moveCount = 0;
    }

	private void clear()
	{// 0 -> boş, 1 -> x, 2 -> o
		squares= new int[]{0,0,0,0,0,0,0,0,0};
        playerOrder = 1;
        xOrder = 1;
        oOrder = 1;
        moveCount = 0;
        
        for(int i=0; i<5; i++){
             
            Rigidbody rbx = GameObject.Find("x"+(i+1)).GetComponent<Rigidbody>();
            rbx.MovePosition(xSignPositions[i]);
             
            Rigidbody rbo = GameObject.Find("o"+(i+1)).GetComponent<Rigidbody>();
            rbo.MovePosition(oSignPositions[i]);
        }
	}

    
    void Update()
    {
       int squareNumber =  getClickedSquare();
       if ( squareNumber >=0 && squareNumber < 9)
       {
           if(isSquareEmpty(squareNumber)){
             
               Renderer  renderer =  getSquareRenderer(squareNumber);
               renderer.material = yellow;

               if(playerOrder == 1)
               {

                Rigidbody rbx = GameObject.Find("x" + (xOrder)).GetComponent<Rigidbody>();
                rbx.MovePosition(getSquarePosition(getClickedSquare()));
                    //
                    //
                    // TODO: Bu bölgeye x çizimi için gereken kod yazılacak
                    //
                    //    

                    xOrder++;  
                moveCount++;
                squares[squareNumber] = 1;
                playerOrder = 2;
               }
               else if(playerOrder == 2)
               {
                    Rigidbody rbo = GameObject.Find("o" + (oOrder)).GetComponent<Rigidbody>();
                    rbo.MovePosition(getSquarePosition(getClickedSquare()));
                    //
                    //
                    // TODO: Bu bölgeye O çizimi için gereken kod yazılacak
                    //
                    //  


                    oOrder++;  
                moveCount++;
                squares[squareNumber] = 2;
                playerOrder = 1;
               }
 
             switch(checkBoardState()){
                 case 0: Debug.Log("Oyun sırası "+playerOrder+". oyuncuda."); break;
                 case 1: Debug.Log("Oyunu "+playerOrder+". oyuncu kazandı."); clear(); break;
                 case 2: Debug.Log("Oyunu "+playerOrder+". oyuncu kazandı."); clear(); break;
                 case 3: Debug.Log("Oyunu berabere bitti."); clear(); break;
             }

           }else{

               Renderer  renderer =  getSquareRenderer(squareNumber);
               renderer.material = red;

           }
       }
    }

    // TODO: tahtadaki x ve o dizilimleri kontrol edilecek ve
    // kazanan varsa x için 1 o için 2 dönecek
    // kazanan yoksa 0 dönecek
    private int checkBoardState()
    {   int result = 0;
        if(playerOrder == 1){
          
            if((squares[0] == 2 && squares[1] == 2 && squares[2] == 2 ) ||
                (squares[3] == 2 && squares[4] == 2 && squares[5] == 2) ||
                (squares[6] == 2 && squares[7] == 2 && squares[8] == 2) ||
                (squares[0] == 2 && squares[3] == 2 && squares[6] == 2) ||
                (squares[1] == 2 && squares[4] == 2 && squares[7] == 2) ||
                (squares[2] == 2 && squares[5] == 2 && squares[8] == 2) ||
                (squares[0] == 2 && squares[4] == 2 && squares[8] == 2) ||
                (squares[2] == 2 && squares[4] == 2 && squares[6] == 2))
            { 
                result = 2;
            }
            //
            //
            // TODO: Bu bölgeye  O için kazanma kontrolü için gereken kod yazılacak
            //
            //     
        }
        else if(playerOrder == 2){

            if ((squares[0] == 1 && squares[1] == 1 && squares[2] == 1) ||
                (squares[3] == 1 && squares[4] == 1 && squares[5] == 1) ||
                (squares[6] == 1 && squares[7] == 1 && squares[8] == 1) ||
                (squares[0] == 1 && squares[3] == 1 && squares[6] == 1) ||
                (squares[1] == 1 && squares[4] == 1 && squares[7] == 1) ||
                (squares[2] == 1 && squares[5] == 1 && squares[8] == 1) ||
                (squares[0] == 1 && squares[4] == 1 && squares[8] == 1) ||
                (squares[2] == 1 && squares[4] == 1 && squares[6] == 1))
            {
                result = 1;
            }
            //
            //
            // TODO: Bu bölgeye  X için kazanma kontrolü için gereken kod yazılacak
            //
            // 
        }
        else if(moveCount == 9){
            result = 3;
        } 
        return result;
    }

    //bu fonksiyon tıkladığın karenin numarasını yolluyor 
    //kullanıldı
    private int getClickedSquare ()
    {
        int hitNumber = -1;
        string objectName = "empty";
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            { 
                objectName = hitInfo.collider.gameObject.name; 
                switch(objectName){
                    case "Kare1": hitNumber = 0 ; break;
                    case "Kare2": hitNumber = 1 ; break;
                    case "Kare3": hitNumber = 2 ; break;
                    case "Kare4": hitNumber = 3 ; break;
                    case "Kare5": hitNumber = 4 ; break;
                    case "Kare6": hitNumber = 5 ; break;
                    case "Kare7": hitNumber = 6 ; break;
                    case "Kare8": hitNumber = 7 ; break;
                    case "Kare9": hitNumber = 8 ; break;
                }
            }
        }
        
        return hitNumber;
    }


    private bool isSquareEmpty(int squareNumber)
    {
        bool SquareEmpty = false;
        if( squares[squareNumber]==0) SquareEmpty = true;
        return SquareEmpty;
    }

    private Renderer getSquareRenderer(int order)
    {
        string squareName = "Kare"+(order+1);
        GameObject square = GameObject.Find(squareName);
        return square.GetComponent<Renderer>();
    } 

    //bu fonksiyon kutuların pozisyonlarını yollar 
    //kullanıldı
    private Vector3 getSquarePosition(int order)
    {
        string squareName = "Kare"+(order+1);
        GameObject square = GameObject.Find(squareName);
        return  square.transform.position;
    } 

    //bu fonksiyon objeye rigitbody ekler 
    private Rigidbody getPlayerSignRigidbody(int order){ 
        string playerSignName = "";
        if(playerOrder == 1){
            playerSignName = "x"+order;
        }else if(playerOrder == 2){
            playerSignName = "o"+order;
        }

        GameObject playerSign = GameObject.Find(playerSignName);
        return playerSign.GetComponent<Rigidbody>();
    }


    private Vector3 getPlayerSignPosition(int order){
        string playerSignName = "";
        if(playerOrder == 1){
            playerSignName = "x"+order;
        }else if(playerOrder == 2){
            playerSignName = "o"+order;
        }

        GameObject playerSign = GameObject.Find(playerSignName);
        return playerSign.transform.position;
    }

 
}
