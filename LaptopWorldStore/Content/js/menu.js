    const openmenubtn = document.getElementById('openmenubutton');
    const closemenubtn = document.getElementById('closemenubutton');
    const hiddenmenu = document.getElementById('hiddenmenu');
    const overplay = document.getElementById('overlay');

    const searchform = document.getElementById('searchinputandbutton');
    const opensearchbtn = document.getElementById('opensearchbutton');
    const closesearchbtn = document.getElementById('closesearchbutton');
    const quatityinshoppingbag = document.getElementById('quatityshoppingbag');
   window.onload = function(){
    Getquatityofproductinshoppingbag();
    Setshoppingbag();
   }
   function Getquatityofproductinshoppingbag(){  
        let shoppingproductlist = [];
        shoppingproductlist = JSON.parse(localStorage.getItem('shoppingproduct')) ;
        if(shoppingproductlist == null){
            quatityinshoppingbag.innerHTML = '0';
        }
        else{
            quatityinshoppingbag.innerHTML = shoppingproductlist.length;
        }
    }
    function Setshoppingbag(){
        let productlist = [];
        productlist = JSON.parse(localStorage.getItem('product'));
        if(productlist == null){
            localStorage.setItem('shoppingproduct', null);
        }
    }
    openmenubtn.addEventListener('click', function open(){
            hiddenmenu.classList.add('active');
    });

   closemenubtn.addEventListener('click',function close(){
       hiddenmenu.classList.remove('active');
   });
   opensearchbtn.addEventListener('click', function opensearch(){
       searchform.style.display = `block`;
       closesearchbtn.style.display = `block`;
   });

   closesearchbtn.addEventListener('click', function closesearch(){
       searchform.style.display = `none`;
       closesearchbtn.style.display = `none`;
   })

   
   function listfocus(){
       overplay.classList.add('overlayactive');
   }
   function listleave(){
    overplay.classList.remove('overlayactive');
   }

