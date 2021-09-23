<template>
  <div class="card text-white bg-dark m-2" style="max-width: 10rem; min-width: 12rem;">
    <!-- REVIEW not sure if these v-ifs are working or not -->
    <div class="card-header text-left" v-if="vault.isPrivate==true">
      Private Vault
    </div>
    <div class="card-header text-left" v-else>
      Public Vault
    </div>
    <div class="card-body">
      <router-link class="mt-auto ml-auto vault" :to="{name: 'VaultDetails', params: {id: vault.id}}">
        <h4 class="card-title" title="Open vault details">
          {{ vault.name }}
        </h4>
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
.vault {
  text-decoration: none;
  color: inherit
}
</style>
