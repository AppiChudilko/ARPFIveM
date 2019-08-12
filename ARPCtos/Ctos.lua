AddEventHandler('CTOSBlackoutStop', function()
  SendNUIMessage({
    type = 'blackoutStop'
  })
end)

AddEventHandler('CTOSBlackoutStart', function()
  SendNUIMessage({
    type = 'blackoutStart'
  })
end)

AddEventHandler('ARPSound:RadioOff', function()
  SendNUIMessage({
    type = 'radioOff'
  })
end)

AddEventHandler('ARPSound:RadioOn', function()
  SendNUIMessage({
    type = 'radioOn'
  })
end)

AddEventHandler('ARPSound:RadioPeer', function()
  SendNUIMessage({
    type = 'radioPeer'
  })
end)