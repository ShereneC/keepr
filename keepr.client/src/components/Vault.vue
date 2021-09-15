<template>
  <div class="card text-white bg-secondary m-2 d-flex" style="max-width: 18rem; min-width: 10rem;">
    <!-- REVIEW not sure if these v-ifs are working or not -->
    <div class="card-header text-left" v-if="vault.isPrivate==true">
      Private Vault
    </div>
    <div class="card-header text-left" v-else>
      Public Vault
    </div>
    <div class="card-body">
      <router-link class="mt-auto ml-auto" :to="{name: 'VaultDetails', params: {id: vault.id}}">
        <h5 class="card-title">
          {{ vault.name }}
        </h5>
      </router-link>
      <p class="card-text">
        {{ vault.description }}
      </p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    vault: {
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
