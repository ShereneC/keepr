<template>
  <div class="about text-center container">
    <!-- Top row for profile information -->
    <div class="row mt-2 mb-5">
      <div class="col-md-2">
        <img class="rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-md-10 d-flex flex-column align-items-start">
        <h1>Welcome {{ account.name }}</h1>
        <h4>Vaults:</h4>
        <h4>Keeps:</h4>
      </div>
    </div>
    <!-- Middle row for profile's vaults - TITLE -->
    <div class="row my-2">
      <div class="col-12 d-flex align-items-center">
        <h3 class="p-0">
          Vaults
        </h3>
        <div class="div">
          <h5 class="text-center"></h5>
        </div>
        <div class="div px-2">
          <span class="fa fa-plus" data-toggle="modal" data-target="#new-vault-modal"></span>
        </div>
      </div>
    </div>
    <!-- Middle row for profile's vaults - CARDS -->
    <div class="row my-2">
      <div class="col-12 d-flex">
        <Vault v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>
    <!-- does it matter where i put this modal call? -->
    <NewVaultModal />
    <!-- Bottom for keeps - TITLE only -->
    <div class="row my-2">
      <div class="col-12 d-flex align-items-center">
        <h3 class="p-0">
          Keeps
        </h3>
        <div class="div">
          <h5 class="text-center"></h5>
        </div>
        <div class="div px-2">
          <span class="fa fa-plus" data-toggle="modal" data-target="#new-keep-modal"></span>
        </div>
      </div>
    </div>
    <!-- Bottom row for profile's keeps - CARDS -->
    <div class="row my-2">
      <div class="col-12">
        <div class="card-columns">
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
    <NewKeepModal />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'

export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await vaultsService.getVaultsByProfileId(route.params.id)
        await keepsService.getKeepsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 300px;
}
</style>
