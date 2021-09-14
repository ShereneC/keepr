<template>
  <div class="keep card img-fluid rounded elevation-1 bg-light my-3 p-3 text-left selectable">
    <img class="card-img-top" :src="keep.img" alt="Card image" style="width:100%">
    <div class="card-img-overlay d-flex">
      <h4 class="card-title text-light titleText mt-auto">
        {{ keep.name }}
      </h4>
      <img :src="keep.creator.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
    </div>
  </div>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.titleText {
  text-shadow: -2px -2px 0 #000;
  // REVIEW  text-shadow: 2px 2px -2 #000; - not happy about this shadow, it will not outline all around the textand it won't let me put it on twice - only takes the first.
}

.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}
</style>
