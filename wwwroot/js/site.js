for(const item of document.querySelectorAll(".item")){
  item.addEventListener('click', e =>{
    item.toggleAttribute()
  }, false);
}

setInterval(async () => {
  var victims = await fetch('/api/victims').then(r => r.json());
  for(const victim of victims){
    const item = document.querySelector(`.item[data-id="${victim.id}"] div`);
    if(item) {
      item.dataset["isAlive"] = victim.isAlive ? "True" : "False";
    }
  }
}, 1000*30);