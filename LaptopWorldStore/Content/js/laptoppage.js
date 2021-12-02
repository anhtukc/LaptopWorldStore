const name = document.getElementById('name');
const id = document.getElementById('id');
const image = document.getElementById('image');
const monitor = document.getElementById('monitor');
const cpu = document.getElementById('cpu');
const gpu = document.getElementById('gpu');
const ram = document.getElementById('ram');
const ssd = document.getElementById('ssd');
const battery = document.getElementById('battery');
const weight = document.getElementById('weight');
const os = document.getElementById('os');
const sellprice = document.getElementById('sellprice');
Getdataproduct();
Writedetailproduct();
Printimage();
function Writedetailproduct(){
    Writename();
    Writeid();
    Writemonitor();
    Writecpu();
    Writegpu();
    Writeram();
    Writessd();
    Writebattery();
    Writesellprice();
    Writeweight();
    Writeos();
}

function Writename(){
    name.innerHTML = product.name;
}

function Writeid(){
   id.innerHTML = product.id;
}

function Writemonitor(){
   monitor.innerHTML = product.monitor;
}

function Writecpu(){
   cpu.innerHTML = product.cpu;
}

function Writegpu(){
   gpu.innerHTML = product.gpu;
}

function Writeram(){
   ram.innerHTML = product.ram;
}

function Writessd(){
   ssd.innerHTML = product.ssd;
}

function Writebattery(){
   battery.innerHTML = product.battery;
}

function Writeos(){
   os.innerHTML = product.os;
}

function Writesellprice(){
    sellprice.innerHTML = product.sellprice + 'đ';
}

function Writeweight(){
   weight.innerHTML = product.weight;
}
function Printimage(){
   image.src = product.picture;
}