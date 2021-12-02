const quatityshoppingbag = document.getElementById('quatityshoppingbag');
const productinshoppingbag = document.getElementById('productinshoppingbag');
const shoppingbag = document.getElementById('shoppingbag');
const namecustomer = document.getElementById('name');
const phonenumber = document.getElementById('phonenumber');
const address = document.getElementById('address');
const orderedbtn = document.getElementById('orderedbtn');
const customerinfo = document.getElementById('customerinfo');
const email = document.getElementById('email');
let totalpaid;
let quantitybuy;

window.onload = function(){
    CheckQuantity();
}

function CheckQuantity() {
    var ListBag = JSON.parse(localStorage.getItem("ProductInBag")) || [];
    quantitybuy = ListBag.length;
    $("#quatityshoppingbag").text("");
    $("#quatityshoppingbag").append("<span>" + quantitybuy + "</span>");
    
}



function Valueof(doc){
    return doc.value;
}
class customers {
    name;
    address;
    email;
    phonenumber;
    constructor() {
        this.name = null;           
        this.address = null;
        this.phonenumber = null;       
        this.email = null;
    }
    #errormessenger ;
    #check;
            

     Checkname(){
        if(Valueof(namecustomer).length < 1){
            this.#errormessenger += 'Tên khách hàng không được bỏ trống\n';
            this.#check = false;
        }
    }

     Checkphonenumber(){
        if(Valueof(phonenumber).length < 10){
            this.#errormessenger += 'Số điện thoại có tối thiểu 10 số\n';
            this.#check = false;
        }
    }

    Checkaddress(){
        if(Valueof(address).length < 10){
            this.#errormessenger += 'Địa chỉ không được bỏ trống\n';
            this.#check = false;
        }
    }

     AppendErrormessenger(){
        if(this.#check == false){
            alert(this.#errormessenger);
        }
        
    }

    Fillctm(){
        this.name = Valueof(namecustomer);
        this.address = Valueof(address);
        this.phonenumber = Valueof(phonenumber);
        this.email = Valueof(email);
        let  customer = {
            'name' : this.name,
            'phonenumber' : this.phonenumber,
            'address' : this.address,
            'email' : this.email 
        };
        return customer;
    }



    Finalresult(){
        return this.#check;
    }

    Setup(){
        this.#errormessenger = '';
        this.#check = true;           
    }

    New(){
        this.#errormessenger = '';
        this.#check = true; 
        this.name = null;
        this.address = null;
        this.phonenumber = null;
        this.email = null;
    }
     
}

