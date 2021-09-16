<template>
  <div class="about text-center container">
    <!-- Top row for profile information -->
    <div class="row mt-2 mb-5">
      <div class="col-md-2">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col-md-10 d-flex flex-column align-items-start">
        <h1>Welcome {{ profile.name }}</h1>
        <h4>Vaults: {{ vaults.length }}</h4>
        <h4>Keeps:  {{ keeps.length }}</h4>
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
          <span class="fa fa-plus text-blue hoverable" title="Click to Add New" data-toggle="modal" data-target="#new-vault-modal"></span>
        </div>
      </div>
    </div>
    <!-- Middle row for profile's vaults - CARDS -->
    <div class="row my-2">
      <div class="col d-flex justify-content-center">
        <div class="card-deck">
          <Vault v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
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
          <span class="fa fa-plus text-blue hoverable" title="Create New Keep" data-toggle="modal" data-target="#new-keep-modal"></span>
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
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profilesService.getProfileById(route.params.id)
        await vaultsService.getVaultsByProfileId(route.params.id)
        await keepsService.getKeepsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 300px;
}
.hoverable {
  cursor: pointer;
}
</style>
