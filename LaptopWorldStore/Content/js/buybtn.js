const buybtn = document.getElementById('buybutton');

window.onload = function () {
    Getdataproduct();
}
let product ;
let shoppingproductlist = [];
class shoppingproduct {
    product;
    quatityinbag;
    constructor(product, quatityinbag){
        this.product = product;
        this.quatityinbag = quatityinbag;
    }
}
function Getdataproduct(){
    product = JSON.parse(localStorage.getItem('viewproduct')) || [];
    shoppingproductlist = JSON.parse(localStorage.getItem('shoppingproduct')) || [];
}

function Setshoppingbag(){
    localStorage.setItem('shoppingproduct', JSON.stringify(shoppingproductlist));
}
function Getquatityofproductinshoppingbag(){  
    if(shoppingproductlist == null){
        quatityinshoppingbag.innerHTML = '0';
    }
    else{
        quatityinshoppingbag.innerHTML = shoppingproductlist.length;
    }
}

function Setbuybutton(){
    if(product.quatity == 0){
        buybtn.disabled = true;
        buybtn.innerHTML = 'Hết hàng';
        buybtn.classList.add('outofstock');
    }
}
buybtn.addEventListener('click', function(){
   
    if(shoppingproductlist.length >= 0 ) {
        let bagproduct = new shoppingproduct(product,1);
        shoppingproductlist.push(bagproduct);
        alert('Thêm thành công');
    }
    else{
        for(let count = 0; count< shoppingproductlist.length; count++){
            if(shoppingproductlist[count].product.id == product.id){
                shoppingproductlist[count].quatityinbag = parseInt(shoppingproductlist[count].quatityinbag) + 1;
            }
        }
        alert('Thêm thành công');
    }
    
    Setshoppingbag();
        Getquatityofproductinshoppingbag();
})

