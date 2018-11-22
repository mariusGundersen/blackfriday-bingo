for(const item of document.querySelectorAll(".item")){
  item.addEventListener('click', e =>{
    item.toggleAttribute()
  }, false);
}

setTimeout(() => document.location.reload(), 1000*30);