<template>
  <div class="keep card rounded elevation-1 bg-light my-3 p-3 text-left selectable">
    {{ keep.creator.picture }}
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

</style>
