const thumbnailslides = document.getElementsByClassName(`thumbnailimageitem`);
const thumbnailslidesdetail = document.getElementsByClassName(`thumbnailbox`);
const thumbnailstate = document.getElementsByClassName(`thumbnailstate`);
const textthumbnail = document.getElementsByClassName(`textthumbnail`);
var index = 1;
autosilide();
    
    function autosilide(){
        let count =0;
        for(count; count<thumbnailslides.length; count++){
            thumbnailslides[count].style.display = `none`;
            thumbnailstate[count].classList.remove(`thumbnailactive`);
            textthumbnail[count].classList.remove(`textactive`);
        }
        if(index>thumbnailslides.length-1)
            index=0;
        thumbnailslides[index].style.display = `block`;
        textthumbnail[index].classList.add(`textactive`);
        
       thumbnailstate[index].classList.add(`thumbnailactive`);
        index++; 
       
        setTimeout(autosilide, 3000);
    };
    function slides(indexinput){
        let count =0;
        for(count; count<thumbnailslides.length; count++){
            thumbnailslides[count].style.display = `none`;
            thumbnailstate[count].classList.remove(`thumbnailactive`);
            textthumbnail[count].classList.remove(`textactive`);
        }  
        index = indexinput;
        thumbnailslides[index].style.display = `block`;
        thumbnailstate[index].classList.add(`thumbnailactive`);
        textthumbnail[index].classList.add(`textactive`);
        
    };