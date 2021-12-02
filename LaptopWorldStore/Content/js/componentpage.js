const name = document.getElementById('name');
const id = document.getElementById('id');
const image = document.getElementById('image');
const sellprice = document.getElementById('sellprice');
Getdataproduct();
Writedetailproduct();
Printimage();
function Writedetailproduct(){
    Writename();
    Writeid();   
   Writesellprice();
}

function Writename(){
    name.innerHTML = product.name;
}

function Writeid(){
   id.innerHTML = product.id;
}
function Printimage(){
    image.src = product.picture;
 }
 function Writesellprice(){
    sellprice.innerHTML = product.sellprice + 'đ';
}