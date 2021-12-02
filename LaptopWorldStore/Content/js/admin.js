const ChangeDisplaytoblock = (Doc) =>{
    Doc.style.display = 'block';
}

const ChangeDisplaytonone = (Doc) =>{
    Doc.style.display = 'none';
}

const DisableTarget = (input) =>{
    input.disabled = true;
}
const EnableTarget = (input) =>{
    input.disabled = false;
}

let Valueof = (docid) =>{
    return docid.value;
}



let Savedatatodatabase = (namedatabase, list)=>{
    return localStorage.setItem(namedatabase,JSON.stringify(list));
}

document.getElementById('signoutbtn').addEventListener('click', function(){
    window.location.href = '../index.html';
    
});